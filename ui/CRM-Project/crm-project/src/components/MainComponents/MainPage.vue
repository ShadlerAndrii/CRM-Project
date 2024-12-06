<template>
    <BuyerMainTable v-if="MainRole == 'buyer' && currentPage == 'main'" v-model:MainUserId="MainUserId" v-model:currentPage="currentPage" v-model:MainToken="MainToken" />
    <SellerMainTable v-if="MainRole == 'seller' && currentPage == 'main'" v-model:MainUserId="MainUserId" v-model:currentPage="currentPage" v-model:MainToken="MainToken" />
</template>

<script>

import BuyerMainTable from './BuyerMainTable.vue';
import SellerMainTable from './SellerMainTable.vue';

export default {
    props: ['role', 'userId', 'userToken'],
    components: {      
        BuyerMainTable,
        SellerMainTable
    },
    data() {
        return {
            currentPage: 'main',
            MainUserId: this.userId,
            MainRole: this.role,
            MainToken: this.userToken,
        }
    },
    watch: {
        role(newRole) {
            this.MainRole = newRole;
        },
        userId(newUserId) {
            this.MainUserId = newUserId;
        },
        userToken(mewUserToken) {
            this.MainToken = mewUserToken;
        }
    }
}
</script>