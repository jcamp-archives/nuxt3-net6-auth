<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
let isEmailConfirmed = $ref(false)
const model = reactive({
  email: '',
  phoneNumber: '',
  isEmailConfirmed: false,
})

try {
  const { data } = await useAsyncData(() => $get('/account/manage/userprofile'))
  Object.assign(model, data.value)
  isEmailConfirmed = model.isEmailConfirmed
} catch {
  navigateTo('/account/login')
}

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/account/manage/changeemail', { body: model })
    message = response.successMessage
  } catch (error: any) {
    handleFormError(error, node)
    errorMessage = error.data.message
  }
}

const sendVerificationEmail = async () => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/account/manage/SendEmailConfirmation')
    message = response.successMessage
  } catch (error: any) {
    errorMessage = error.data.message
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Manage Email</h1>
    <TwAlertSuccess>{{ message }}</TwAlertSuccess>
    <TwAlertDanger>{{ errorMessage }}</TwAlertDanger>

    <TwCard class="mt-8 max-w-lg">
      <div class="grid grid-cols-1 gap-6">
        <FormKit
          v-model="model"
          type="form"
          :submit-attrs="{ inputClass: 'btn' }"
          submit-label="Change Email"
          @submit="submitHandler"
        >
          <FormKit
            id="email"
            disabled
            label="Email"
            type="email"
            validation="required|email"
            input-class="border-gray-300 bg-gray-200"
          />
          <div
            v-if="isEmailConfirmed"
            class="-mt-5 mb-5 flex text-green-600"
          >
            <div class="i-carbon-checkmark h-5 w-5 text-green-600" />
            Confirmed
          </div>
          <div v-if="!isEmailConfirmed">
            <a
              class="-mt-5 mb-5 block cursor-pointer text-blue-500"
              @click.prevent="sendVerificationEmail"
              >Send verification email</a
            >
          </div>
          <FormKit
            id="newEmail"
            type="email"
            validation="required|email"
          />
        </FormKit>
      </div>
    </TwCard>
  </div>
</template>

<style>
.formkit-icon {
  flex-grow: 0;
  flex-shrink: 0;
  display: flex;
  align-self: stretch;
}
.formkit-icon svg {
  max-width: 1em;
  max-height: 1em;
}
</style>
