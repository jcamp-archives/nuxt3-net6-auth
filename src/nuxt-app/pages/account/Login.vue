<script setup lang="ts">
const route = useRoute()
const authStore = useAuthStore()

let message = $ref('')
let errorMessage = $ref('')
let returnUrl = $ref('')
const model = reactive({
  email: '',
  password: '',
})

onMounted(() => {
  returnUrl = route.query.returnUrl as string
  setFocus('email')
})

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const result = await $postBody('/account/login', model)
    authStore.login(result.token)
    if (returnUrl) navigateTo(returnUrl)
    else navigateTo('/')
  } catch (error: any) {
    console.log(error.data)
    const result = error.data
    if (result.requiresTwoFactor) {
      navigateTo({
        path: '/account/loginwith2fa',
        query: { returnUrl },
      })
    } else if (result.requiresEmailConfirmation) {
      navigateTo({
        path: '/account/ResendEmailConfirmation',
        query: { returnUrl },
      })
    } else if (result.isLockedOut) {
      navigateTo('/Account/Lockout')
    } else {
      errorMessage = handleFormError(error, node)
    }
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Login</h1>

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
          submit-label="Login"
          :submit-attrs="{ inputClass: 'btn' }"
          @submit="submitHandler"
        >
          <FormKit
            id="email"
            type="email"
            validation="required|email"
          />
          <FormKit
            id="password"
            type="password"
            validation="required"
          />
        </FormKit>
      </div>
    </TwCard>

    <div class="form-group">
      <p>
        <nuxt-link to="/Account/ForgotPassword">
          Forgot your password?
        </nuxt-link>
      </p>
      <p>
        <nuxt-link to="/Account/Register"> Register as a new user </nuxt-link>
      </p>
      <p>
        <nuxt-link to="/Account/ResendEmailConfirmation">
          Resend email confirmation
        </nuxt-link>
      </p>
    </div>
  </div>
</template>
