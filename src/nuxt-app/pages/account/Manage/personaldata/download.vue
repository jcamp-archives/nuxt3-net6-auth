<script setup lang="ts">
let body = $ref('')

try {
  const { data } = await useAsyncData(() =>
    $post('/account/manage/downloadpersonaldata', { body: {} })
  )
  body = data.value.jsonData
} catch {
  navigateTo('/account/login')
}
</script>

<template>
  <div>
    <h1 clas="text-xl">Download Personal Data</h1>

    <TwCard>
      <textarea
        v-model="body"
        class="block w-full"
        style="height: 20rem"
      ></textarea>
    </TwCard>
  </div>
</template>
