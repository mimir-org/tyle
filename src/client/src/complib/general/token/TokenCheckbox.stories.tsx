import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TokenCheckbox } from "./TokenCheckbox";

export default {
  title: "General/TokenCheckbox",
  component: TokenCheckbox,
} as ComponentMeta<typeof TokenCheckbox>;

const Template: ComponentStory<typeof TokenCheckbox> = (args) => <TokenCheckbox {...args} />;

export const Primary = Template.bind({});
Primary.args = {
  children: "Primary token (default)",
  variant: "primary",
};

export const PrimaryChecked = Template.bind({});
PrimaryChecked.args = {
  children: "Primary token (default)",
  variant: "primary",
  checked: true,
};

export const Secondary = Template.bind({});
Secondary.args = {
  children: "Secondary token",
  variant: "secondary",
};

export const SecondaryChecked = Template.bind({});
SecondaryChecked.args = {
  children: "Secondary token",
  variant: "secondary",
  checked: true,
};
