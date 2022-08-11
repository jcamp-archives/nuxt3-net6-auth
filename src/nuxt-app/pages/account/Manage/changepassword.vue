<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
const model = reactive({ oldPassword: '', password: '', confirmPassword: '' })

onMounted(() => setFocus('oldPassword'))

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const response = await $postBody('/account/manage/changepassword', model)
    message = response.successMessage
  } catch (error: any) {
    errorMessage = handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Change Password</h1>
    <TwAlertSuccess>{{ message }}</TwAlertSuccess>
    <TwAlertDanger>{{ errorMessage }}</TwAlertDanger>

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
            :validation="valRules.password"
          />
          <FormKit
            id="password"
            type="password"
            help="Enter a new password"
            :validation="valRules.password"
            :validation-messages="valRules.passwordMessages"
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
