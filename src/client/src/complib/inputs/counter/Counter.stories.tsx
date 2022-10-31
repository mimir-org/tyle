import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Counter } from "complib/inputs/counter/Counter";

export default {
  title: "Inputs/Counter",
  component: Counter,
} as ComponentMeta<typeof Counter>;

const Template: ComponentStory<typeof Counter> = (args) => <Counter {...args} />;

export const Default = Template.bind({});

export const Disabled = Template.bind({});
Disabled.args = {
  id: "somethingunique",
  disabled: true,
};
