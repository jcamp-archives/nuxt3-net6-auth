<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
let codes = $ref([])

const generateCodes = async () => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/account/manage/mfageneratecodes', {
      body: {},
    })
    codes = response.recoveryCodes
    message = response.successMessage
  } catch (error: any) {
    errorMessage = error.data.errorMessage
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Generate Recovery Codes</h1>
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
      <p v-if="codes.length > 0">
        <span class="glyphicon glyphicon-warning-sign"></span>
        <strong>Put these codes in a safe place.</strong>
      </p>
      <p v-if="codes.length > 0">
        If you lose your device and don't have the recovery codes you will lose
        access to your account.
      </p>
      <p v-if="codes.length == 0">
        Generating new recovery codes does not change the keys used in
        authenticator apps. If you wish to change the key used in an
        authenticator app you should
        <router-link to="/Account/Manage/TwoFactor/Reset"
          >reset your authenticator keys.</router-link
        >
      </p>
    </div>
    <div v-if="codes.length == 0">
      <button
        class="btn mt-4 bg-red-400 hover:bg-red-600"
        @click.prevent="generateCodes"
      >
        Generate Recovery Codes
      </button>
    </div>
    <div
      v-if="codes.length > 0"
      class="row"
    >
      <div class="col-md-12">
        <span
          v-for="i in codes.length / 2"
          :key="i"
        >
          <code class="recovery-code">{{ codes[i * 2 - 2] }}</code
          ><text>&nbsp;</text
          ><code class="recovery-code">{{ codes[i * 2 - 1] }}</code
          ><br />
        </span>
      </div>
    </div>
  </div>
</template>
