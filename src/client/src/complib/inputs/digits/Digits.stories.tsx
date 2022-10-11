import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Digits } from "./Digits";

export default {
  title: "Inputs/Digits",
  component: Digits,
} as ComponentMeta<typeof Digits>;

const Template: ComponentStory<typeof Digits> = (args) => <Digits {...args} />;

export const Default = Template.bind({});

