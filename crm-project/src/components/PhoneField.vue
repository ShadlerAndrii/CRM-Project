<template>
    <div class="input-group mb-3">
        <!-- Вказуємо маску для телефонного номера через v-mask -->
        <input type="text" class="form-control" placeholder="Your Phone number" 
               v-model="localPhone" v-mask="'+38(0##)-###-##-##'" 
               @input="validateInput" required />
        <div v-if="!isValid" style="color: red;">Incorrect data format</div>
    </div>
</template>

<script>
export default {
    props: ['phone'],
    data() {
        return {
            localPhone: this.phone,
            isValid: true,
        };
    },
    watch: {
        phone(newPhone) {
            this.localPhone = newPhone;
        },
        localPhone(newLocalPhone) {
            this.$emit('update:phone', newLocalPhone);
        }
    },
    methods: {
        validateInput() {
            const regex = /^\+38\(0\d{2}\)-\d{3}-\d{2}-\d{2}$/;
            this.isValid = regex.test(this.localPhone);
        }
    }
};
</script>
