<script setup lang="ts">
import type { FormKitNode } from '@formkit/core'
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
  document.getElementsByName('email')[0]?.focus()
})

const submitHandler = async (_data: any, node?: FormKitNode) => {
  message = ''
  errorMessage = ''
  try {
    const result = await $post('/account/login', { body: model })
    authStore.login(result.token)
    if (returnUrl) navigateTo(returnUrl)
    else navigateTo('/')
  } catch (error: any) {
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
      node?.setErrors(error.data.errors, error.data.validationErrors)

      setTimeout(() => {
        const x = document.getElementsByName(
          Object.keys(error.data.validationErrors)[0]
        )[0]
        if (x) x.focus()
      }, 200)
    }
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Login</h1>

    <TwAlertSuccess v-if="message">
      {{ message }}
    </TwAlertSuccess>
    <TwAlertDanger v-if="errorMessage">
      {{ errorMessage }}
    </TwAlertDanger>

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
            name="email"
            label="Email"
            type="email"
            validation="required|email"
          />
          <FormKit
            type="password"
            name="password"
            value="super-secret"
            label="Password"
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
