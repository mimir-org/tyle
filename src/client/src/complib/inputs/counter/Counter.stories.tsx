import { Meta, StoryFn } from "@storybook/react";
import { Counter } from "complib/inputs/counter/Counter";

export default {
  title: "Inputs/Counter",
  component: Counter,
} as Meta<typeof Counter>;

const Template: StoryFn<typeof Counter> = (args) => <Counter {...args} />;

export const Default = Template.bind({});

export const Disabled = Template.bind({});
Disabled.args = {
  id: "somethingunique",
  disabled: true,
};
