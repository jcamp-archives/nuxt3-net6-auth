<script setup lang="ts">
const route = useRoute()

let message = $ref('')
let errorMessage = $ref('')

const model = reactive({
  code: '',
  email: '',
  password: '',
  confirmPassword: '',
})

onMounted(() => {
  const email = route.query.email
  const code = route.query.code

  if (!email || !code) navigateTo('/')

  model.email = email as string
  model.code = code as string

  document.getElementById('password')?.focus()
})

const submitHandler = async (_data: any, node: any) => {
  message = ''
  try {
    const response = await $post('/account/resetpassword', { body: model })
    navigateTo('/account/ResetPasswordConfirmation')
  } catch (error: any) {
    handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Reset Password</h1>

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
          :submit-attrs="{ inputClass: 'btn' }"
          @submit="submitHandler"
        >
          <FormKit
            id="password"
            type="password"
            value="super-secret"
            help="Enter a new password"
            validation="required"
          />
          <FormKit
            id="confirmPassword"
            type="password"
            help="Confirm your new password"
            validation="required|confirm:password"
            validation-label="Password confirmation"
          />
        </FormKit>
      </div>
    </TwCard>
  </div>
</template>
