<script setup lang="ts">
const model = reactive({ password: '' })
let message = $ref('')
let errorMessage = $ref('')

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    await $postBody('/account/manage/deletepersonaldata', model)
    navigateTo('/account/logout')
  } catch (error: any) {
    errorMessage = handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 clas="text-xl">Delete Personal Data</h1>
    <TwAlertSuccess>{{ message }}</TwAlertSuccess>
    <TwAlertDanger>{{ errorMessage }}</TwAlertDanger>
    <TwAlertDanger>
      Deleting this data will permanently remove your account, and this cannot
      be recovered.
    </TwAlertDanger>
    <FormKit
      v-model="model"
      type="form"
      submit-label="Delete data and close my account"
      :submit-attrs="{ inputClass: 'btn bg-red-400' }"
      @submit="submitHandler"
    >
      <FormKit
        id="password"
        type="password"
        validation="required"
      />
    </FormKit>
  </div>
</template>
