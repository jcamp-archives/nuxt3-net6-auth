<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')

const disableMfa = async () => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/api/account/manage/mfadisable', {
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
    <h1 class="text-xl">Disable multi-factor authentication (MFA)</h1>
    <TwAlertSuccess v-if="message">
      {{ message }}
    </TwAlertSuccess>
    <TwAlertDanger v-if="errorMessage">
      {{ errorMessage }}
    </TwAlertDanger>
    <div
      class="alert alert-warning"
      role="alert"
    >
      <p>
        <strong>This action only disables MFA.</strong>
      </p>
      <p>
        Disabling MFA does not change the keys used in authenticator apps. If
        you wish to change the key used in an authenticator app you should
        <nuxt-link to="/account/manage/twofactor/reset"
          >reset your authenticator keys.</nuxt-link
        >
      </p>
    </div>

    <div>
      <button
        class="btn mt-4 bg-red-400 hover:bg-red-600"
        @click.prevent="disableMfa"
      >
        Disable MFA
      </button>
    </div>
  </div>
</template>
