<template>
    <div v-bind:class="getMessageClass()">
      <div>
        <ul class="error-list">
          <li v-for="(msg, index) in messages" :key="index">
            <small>{{ msg }}</small>
          </li>
        </ul>
      </div>
      </div>
  </template>
  
  <script setup lang="ts">
  import { withDefaults, defineProps } from "vue"

  interface Props{
      messages: string[], 
      isError: boolean,
  }
    const props = withDefaults(defineProps<Props>(),{
      messages : ()=>[],
    });
  
    const getMessageClass = () => {
        if (!props.messages) return;
  
        if (props.messages.length == 0) {
          return "d-none";
        }
  
        return props.isError
          ? "text-danger text-justify"
          : "text-success text-center";
      }
      console.log(props);
  
  </script>
  
  <style>
.error-list {
  list-style-type: none;
  padding: 0;
}
</style>