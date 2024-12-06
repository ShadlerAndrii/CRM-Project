<template>
  <div id="app">
    <RegisterPage v-if="currentPage == 'register'" v-model:currentPage="currentPage" />
    <LoginPage v-if="!isAuthorized && currentPage == 'login'" v-model:role="role" v-model:isAuthorized="isAuthorized"
      v-model:userId="userId" v-model:currentPage="currentPage" v-model:userToken="userToken" />
    <MainPage v-if="isAuthorized == true" v-model:role="role" v-model:userId="userId" v-model:userToken="userToken" />
  </div>
</template>

<script>
import RegisterPage from './components/RegistrationComponents/RegisterPage.vue';
import LoginPage from './components/LoginComponents/LoginPage.vue';
import MainPage from './components/MainComponents/MainPage.vue';

export default {
  data() {
    return {
      currentPage: 'login', // Початкова сторінка
      role: '',
      userId: '',
      isAuthorized: false,
      userToken: '',
    };
  },
  components: {
    RegisterPage,
    LoginPage,
    MainPage
  },
  watch: {
    isAuthorized(newIsAuthorized) {
      this.isAuthorized = newIsAuthorized;
    },
    role(newRole) {
      this.role = newRole;
    },
    userId(newUserId) {
      this.userId = newUserId;
    },
    currentPage(newPage) {
      this.currentPage = newPage;
    },
    userToken(newToken) {
      this.userToken = newToken;
    }
  }
}
</script>

<style scoped>
button {
  margin: 5px;
}
</style>
