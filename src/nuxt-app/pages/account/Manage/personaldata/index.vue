<template>
  <div>
    <h2>Personal Data</h2>

    <div class="row">
      <div class="col-md-8">
        <p>
          Your account contains personal data that you have given us. This page
          allows you to download or delete that data.
        </p>
        <p>
          <strong
            >Deleting this data will permanently remove your account, and this
            cannot be recovered.</strong
          >
        </p>
        <p>
          <nuxt-link
            to="/account/manage/personaldata/download"
            class="btn mt-4"
            >Download</nuxt-link
          >
          <nuxt-link
            to="/account/manage/personaldata/delete"
            class="btn ml-4 bg-red-500"
            >Delete</nuxt-link
          >
        </p>
      </div>
    </div>
  </div>
</template>
