<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
const model = reactive({
  hasAuthenticator: false,
  recoveryCodesLeft: 0,
  isMfaEnabled: false,
  isMachineRemembered: false,
})

try {
  const { data } = await useAsyncData(() => $api('/account/manage/mfainfo'), {
    initialCache: false,
    server: false,
  })
  Object.assign(model, data.value)
} catch {
  navigateTo('/account/login')
}

const forgetBrowser = async () => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/api/account/manage/mfaforgetbrowser', {
      body: {},
    })
    message = response.successMessage
  } catch (error: any) {
    errorMessage = error.data.errorMessage
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Two Factor Authentication</h1>
    <TwAlertSuccess v-if="message">
      {{ message }}
    </TwAlertSuccess>
    <TwAlertDanger v-if="errorMessage">
      {{ errorMessage }}
    </TwAlertDanger>
    <div v-if="model.isMfaEnabled">
      <div
        v-if="model.recoveryCodesLeft === 0"
        class="alert alert-danger"
      >
        <strong>You have no recovery codes left.</strong>
        <p>
          You must
          <nuxt-link to="/Account/Manage/TwoFactor/RecoveryCodes"
            >generate a new set of recovery codes</nuxt-link
          >
          before you can log in with a recovery code.
        </p>
      </div>
      <div
        v-else-if="model.recoveryCodesLeft === 1"
        class="alert alert-danger"
      >
        <strong>You have 1 recovery code left.</strong>
        <p>
          You can
          <nuxt-link to="/Account/Manage/TwoFactor/RecoveryCodes"
            >generate a new set of recovery codes</nuxt-link
          >.
        </p>
      </div>
      <div
        v-else-if="model.recoveryCodesLeft <= 3"
        class="alert alert-warning"
      >
        <strong
          >You have {{ model.recoveryCodesLeft }} recovery codes left.</strong
        >
        <p>
          You should
          <nuxt-link to="/Account/Manage/TwoFactor/RecoveryCodes"
            >generate a new set of recovery codes</nuxt-link
          >.
        </p>
      </div>

      <button
        v-if="model.isMachineRemembered"
        style="margin-right: 4px"
        class="btn btn-primary"
        @click.prevent="forgetBrowser"
      >
        Forget this browser
      </button>

      <nuxt-link
        to="/Account/Manage/TwoFactor/Disable"
        class="btn btn-primary mr-2"
        >Disable 2FA</nuxt-link
      >
      <nuxt-link
        to="/Account/Manage/TwoFactor/RecoveryCodes"
        class="btn btn-primary"
        >Reset recovery codes</nuxt-link
      >
    </div>

    <h5>Authenticator app</h5>
    <nuxt-link
      v-if="!model.hasAuthenticator"
      to="/Account/Manage/TwoFactor/Enable"
      class="btn btn-primary mr-2"
      >Add authenticator app</nuxt-link
    >
    <nuxt-link
      v-if="model.hasAuthenticator"
      to="/Account/Manage/TwoFactor/Enable"
      class="btn btn-primary mr-2"
      >Setup authenticator app</nuxt-link
    >
    <nuxt-link
      v-if="model.hasAuthenticator"
      to="/Account/Manage/TwoFactor/Reset"
      class="btn btn-primary"
      >Reset authenticator app</nuxt-link
    >
  </div>
</template>
