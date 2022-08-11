<script setup lang="ts">
const route = useRoute()
const authStore = useAuthStore()

let message = $ref('')
let errorMessage = $ref('')
let returnUrl = $ref('')
const model = reactive({ twoFactorCode: '', rememberMachine: false })

onMounted(async () => {
  returnUrl = route.query.returnUrl as string
  try {
    await $postBody('/api/account/checkmfa', {})
  } catch (ex) {
    navigateTo({ path: '/account/login', query: { returnUrl } })
  }
  setFocus('recoveryCode')
})

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const result = await $postBody('/account/loginrecovery', model)

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
    <h1 class="text-2xl">Recovery code verification</h1>

    <p>
      You have requested to log in with a recovery code. This login will not be
      remembered until you provide an authenticator app code at log in or
      disable 2FA and log in again.
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
            id="recoveryCode"
            type="text"
            validation="required"
          />
        </FormKit>
      </div>
    </TwCard>
  </div>
</template>
