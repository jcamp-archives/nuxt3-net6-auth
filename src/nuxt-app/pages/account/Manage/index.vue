<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
const model = reactive({
  email: '',
  phoneNumber: '',
  isEmailConfirmed: false,
})

try {
  const { data } = await useAsyncData(() => $get('/account/manage/userprofile'))
  Object.assign(model, data.value)
} catch {
  navigateTo('/account/login')
}

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const response = await $postBody('/account/manage/userprofile', model)
    message = 'profile updated'
  } catch (error: any) {
    errorMessage = handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Profile</h1>
    <TwAlertSuccess>{{ message }}</TwAlertSuccess>
    <TwAlertDanger>{{ errorMessage }}</TwAlertDanger>

    <TwCard class="mt-8 max-w-lg">
      <div class="grid grid-cols-1 gap-6">
        <FormKit
          v-model="model"
          type="form"
          :submit-attrs="{ inputClass: 'btn' }"
          submit-label="Save"
          @submit="submitHandler"
        >
          <FormKit
            id="email"
            disabled
            label="User Name"
            type="email"
            validation="required|email"
            input-class="mt-1 block w-full border-gray-300 bg-gray-200"
          />
          <FormKit
            id="phoneNumber"
            type="text"
          />
        </FormKit>
      </div>
    </TwCard>
  </div>
</template>
