<template>
  <div v-bind:class="getMessageClass()">
    <div v-if="messages.length > 1">
      <ul>
        <li v-for="(msg, index) in messages" :key="index">
          <small>{{ msg }}</small>
        </li>
      </ul>
    </div>
    <div v-else>
      <small>{{ messages[0] }}</small>
    </div>
  </div>
</template>

<script>
export default {
  name: "ValidationMessage",
  props: {
    messages: {
      type: Array,
      required: true,
    },
    isError: {
      type: Boolean,
      required: true,
    },
  },

  methods: {
    getMessageClass() {
      if (this.messages.length == 0) {
        return "d-none";
      }

      return this.isError ? "text-danger text-left" : "text-success text-left";
    },
  },
};
</script>

<style></style>
