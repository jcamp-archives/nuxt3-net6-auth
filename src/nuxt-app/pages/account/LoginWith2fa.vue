<script setup lang="ts">
const route = useRoute()
const authStore = useAuthStore()

let message = $ref('')
let errorMessage = $ref('')
let returnUrl = $ref('')
const model = reactive({ twoFactorCode: '', rememberMachine: false })

// onMounted(async () => {
//   setTimeout(async () => {
//     returnUrl = route.query.returnUrl as string
//     try {
//       await $postBody('/api/account/checkmfa', {})
//     } catch (ex) {
//       navigateTo({ path: '/account/login', query: { returnUrl } })
//     }
//   }, 100)
// })
onMounted(async () => {
  returnUrl = route.query.returnUrl as string
  try {
    await $postBody('/api/account/checkmfa', {})
  } catch (ex) {
    navigateTo({ path: '/account/login', query: { returnUrl } })
  }
  setFocus('twoFactorCode')
})

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const result = await $postBody('/account/loginmfa', model)

    if (result.isSuccess) {
      authStore.login(result.token)
      if (returnUrl) navigateTo(returnUrl)
      else navigateTo('/')
    }
  } catch (error: any) {
    errorMessage = handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Two-factor authentication</h1>

    <p>
      Your login is protected with an authenticator app. Enter your
      authenticator code below.
    </p>

    <TwAlertSuccess>{{ message }}</TwAlertSuccess>
    <TwAlertDanger>{{ errorMessage }}</TwAlertDanger>

    <TwCard
      title="Please enter your details"
      class="mt-8 max-w-lg"
    >
      <div class="grid grid-cols-1 gap-6">
        <FormKit
          v-model="model"
          type="form"
          submit-label="Submit"
          :submit-attrs="{ inputClass: 'btn' }"
          @submit="submitHandler"
        >
          <FormKit
            id="twoFactorCode"
            type="text"
            label="Verification Code"
            :validation="valRules.mfaCode"
          />
          <FormKit
            id="rememberMachine"
            type="checkbox"
            label="Remember this machine"
          />
        </FormKit>
      </div>
    </TwCard>

    <div class="form-group">
      <p>
        Don't have access to your authenticator device? You can
        <nuxt-link
          :to="{ path: '/account/LoginWithRecoveryCode', query: { returnUrl } }"
        >
          log in with a recovery code </nuxt-link
        >.
      </p>
    </div>
  </div>
</template>
