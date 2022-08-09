<script setup lang="ts">
import type { FormKitNode } from '@formkit/core'

let message = $ref('')
const model = reactive({
  email: '',
  password: '',
  confirmPassword: '',
  returnUrl: '',
})

onMounted(() => {
  document.getElementsByName('email')[0]?.focus()
})

const submitHandler = async (_data: any, node?: FormKitNode) => {
  message = ''
  try {
    const response = await $post('/account/register', { body: model })
    navigateTo('/account/registerconfirmation')
  } catch (error: any) {
    node?.setErrors(error.data.errors, error.data.validationErrors)

    setTimeout(() => {
      const x = document.getElementsByName(
        Object.keys(error.data.validationErrors)[0]
      )[0]
      if (x) x.focus()
    }, 200)
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
            help="Enter a new password"
            validation="required"
          />
          <FormKit
            type="password"
            name="confirmPassword"
            label="Confirm password"
            help="Confirm your new password"
            validation="required|confirm:password"
            validation-label="Password confirmation"
          />
        </FormKit>
      </div>
    </TwCard>
  </div>
</template>
