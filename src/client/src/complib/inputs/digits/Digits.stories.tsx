import { Meta, StoryFn } from "@storybook/react";
import { Digits } from "complib/inputs/digits/Digits";

export default {
  title: "Inputs/Digits",
  component: Digits,
} as Meta<typeof Digits>;

const Template: StoryFn<typeof Digits> = (args) => <Digits {...args} />;

export const Default = Template.bind({});
