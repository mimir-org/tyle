import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TokenCheckbox } from "complib/general/token/checkbox/TokenCheckbox";

export default {
  title: "General/TokenCheckbox",
  component: TokenCheckbox,
} as ComponentMeta<typeof TokenCheckbox>;

const Template: ComponentStory<typeof TokenCheckbox> = (args) => <TokenCheckbox {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Token",
};

export const Checked = Template.bind({});
Checked.args = {
  children: "Token",
  checked: true,
};
