<script setup lang="ts">
let message = $ref('')
const model = reactive({
  email: '',
  password: '',
  confirmPassword: '',
  returnUrl: '',
})

onMounted(() => {
  document.getElementById('email')?.focus()
})

const submitHandler = async (_data: any, node: any) => {
  message = ''
  try {
    const response = await $post('/account/register', { body: model })
    navigateTo('/account/registerconfirmation')
  } catch (error: any) {
    handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Register</h1>

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
            id="email"
            type="email"
            validation="required|email"
          />
          <FormKit
            id="password"
            type="password"
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
