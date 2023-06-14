import { Meta, StoryFn } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { Input } from "complib/inputs/input/Input";

export default {
  title: "Inputs/Input",
  component: Input,
} as Meta<typeof Input>;

const Template: StoryFn<typeof Input> = (args) => <Input {...args} />;

export const Text = Template.bind({});
Text.args = {
  defaultValue: "Some input",
  placeholder: "placeholder",
  type: "text",
};

export const Number = Template.bind({});
Number.args = {
  defaultValue: 101,
  placeholder: "123",
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

export const WithIcon = Template.bind({});
WithIcon.args = {
  defaultValue: "Some input",
  placeholder: "placeholder",
  type: "text",
  icon: LibraryIcon,
};
