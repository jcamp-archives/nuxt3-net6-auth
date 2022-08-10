<script setup lang="ts">
let message = $ref('')
let errorMessage = $ref('')
const model = reactive({
  sharedKey: '',
  authenticatorUri: '',
  qrCodeBase64: '',
})
const codeModel = reactive({ verificationCode: '' })

try {
  const { data } = await useAsyncData(() => $api('/account/manage/mfaenable'))
  Object.assign(model, data.value)
} catch {
  navigateTo('/account/login')
}

onMounted(() => setFocus('verificationCode'))

const submitHandler = async (_data: any, node: any) => {
  message = ''
  errorMessage = ''
  try {
    const response = await $post('/account/manage/mfaenable', {
      body: {
        verificationCode: codeModel.verificationCode,
      },
    })
    message = response.successMessage
  } catch (error: any) {
    errorMessage = error.data.message
    handleFormError(error, node)
  }
}
</script>

<template>
  <div>
    <h1 class="text-xl">Enable multi-factor authentication (MFA)</h1>
    <TwAlertSuccess v-if="message">
      {{ message }}
    </TwAlertSuccess>
    <TwAlertDanger v-if="errorMessage">
      {{ errorMessage }}
    </TwAlertDanger>
    <div v-if="model.qrCodeBase64">
      <p>To use an authenticator app go through the following steps:</p>
      <ol class="list">
        <li>
          <p>
            Download a two-factor authenticator app like Microsoft Authenticator
            for
            <a href="https://go.microsoft.com/fwlink/?Linkid=825072">Android</a>
            and
            <a href="https://go.microsoft.com/fwlink/?Linkid=825073">iOS</a> or
            Google Authenticator for
            <a
              href="https://play.google.com/store/apps/details?id=com.google.android.apps.authenticator2&amp;hl=en"
              >Android</a
            >
            and
            <a
              href="https://itunes.apple.com/us/app/google-authenticator/id388497605?mt=8"
              >iOS</a
            >.
          </p>
        </li>
        <li>
          <p>
            Scan the QR Code or enter this key
            <kbd>{{ model.sharedKey }}</kbd> into your two factor authenticator
            app. Spaces and casing do not matter.
          </p>
          <img :src="`data:image/png;base64,${model.qrCodeBase64}`" />
        </li>
        <li>
          <p>
            Once you have scanned the QR code or input the key above, your two
            factor authentication app will provide you with a unique code. Enter
            the code in the confirmation box below.
          </p>
          <div class="row">
            <div class="col-md-6">
              <FormKit
                v-model="codeModel"
                type="form"
                submit-label="Verify"
                :submit-attrs="{ inputClass: 'btn' }"
                @submit="submitHandler"
              >
                <FormKit
                  id="verificationCode"
                  type="text"
                  validation="required"
                />
              </FormKit>
            </div>
          </div>
        </li>
      </ol>
    </div>
  </div>
</template>
