<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
const model = reactive({ oldPassword: '', password: '', confirmPassword: '' })

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/account/manage/changepassword', {
      body: model,
    })
    message = response.successMessage
  } catch (error: any) {
    handleFormError(error, node)
    errorMessage = error.data.message
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Change Password</h1>
    <TwAlertSuccess v-if="message">
      {{ message }}
    </TwAlertSuccess>
    <TwAlertDanger v-if="errorMessage">
      {{ errorMessage }}
    </TwAlertDanger>

    <TwCard class="mt-8 max-w-lg">
      <div class="grid grid-cols-1 gap-6">
        <FormKit
          v-model="model"
          type="form"
          :submit-attrs="{ inputClass: 'btn' }"
          submit-label="Update Password"
          @submit="submitHandler"
        >
          <FormKit
            id="oldPassword"
            type="password"
            validation="required"
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
