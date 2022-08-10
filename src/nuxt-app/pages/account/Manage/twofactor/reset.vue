<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')

const resetAuth = async () => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/api/account/manage/mfaresetkey', {
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
    <h1 class="text-xl">Reset authenticator key</h1>
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
        <strong
          >If you reset your authenticator key your authenticator app will not
          work until you reconfigure it.</strong
        >
      </p>
      <p>
        This process disables 2FA until you verify your authenticator app. If
        you do not complete your authenticator app configuration you may lose
        access to your account.
      </p>
    </div>

    <div>
      <button
        class="btn mt-4 bg-red-400 hover:bg-red-600"
        @click.prevent="resetAuth"
      >
        Reset authenticator key
      </button>
    </div>
  </div>
</template>
