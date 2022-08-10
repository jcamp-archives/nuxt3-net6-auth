<script setup lang="ts">
let message = $ref('')
const model = reactive({ email: '' })

const submitHandler = async (_data: any, node: any) => {
  message = ''
  try {
    const response = await $post('/account/forgotpassword', { body: model })
    navigateTo('/account/ForgotPasswordConfirmation')
  } catch (error: any) {
    handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-2xl">Forgot Password</h1>

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
        </FormKit>
      </div>
    </TwCard>
  </div>
</template>
