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

  setFocus('password')
})

const submitHandler = async (_data: any, node: any) => {
  message = ''
  try {
    const response = await $postBody('/account/resetpassword', model)
    navigateTo('/account/ResetPasswordConfirmation')
  } catch (error: any) {
    errorMessage = handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Reset Password</h1>

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
