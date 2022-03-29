import { ComponentMeta, ComponentStory } from "@storybook/react";
import StandardInput from "./Input";

export default {
  title: "Library/Atoms/Input",
  component: StandardInput,
} as ComponentMeta<typeof StandardInput>;

const Template: ComponentStory<typeof StandardInput> = (args) => <StandardInput {...args} />;

export const Text = Template.bind({});
Text.args = {
  defaultValue: "Some input",
  placeholder: "placeholder",
  type: "text",
};

export const Number = Template.bind({});
Number.args = {
  defaultValue: 101,
  placeholder: 123,
  type: "number",
};

export const Email = Template.bind({});
Email.args = {
  defaultValue: "jane.smith@organization.com",
  placeholder: "jane.smith@organization.com",
  type: "email",
};

export const Password = Template.bind({});
Password.args = {
  defaultValue: "youbetterchooseastrongone",
  placeholder: "********************",
  type: "password",
};
