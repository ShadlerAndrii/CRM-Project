<template>
    <div id="app">
        <FormComponent v-model:users="users" />
        <UserTable v-model:users="users" />
    </div>
</template>

<script>
import FormComponent from './FormComponent.vue';
import UserTable from './UserTable.vue';

export default {
    components: {
        FormComponent,
        UserTable,
    },
    data() {
        return {
            users: [],
        };
    },
};
</script>