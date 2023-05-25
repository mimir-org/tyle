import { Meta, StoryFn } from "@storybook/react";
import { FormField } from "complib/form/FormField";
import { Text } from "complib/inputs/input/Input.stories";

export default {
  title: "Form/FormField",
  component: FormField,
} as Meta<typeof FormField>;

const Template: StoryFn<typeof FormField> = (args) => <FormField {...args} />;

export const Default = Template.bind({});
Default.args = {
  label: "Label for input",
  children: <Text />,
};

export const WithError = Template.bind({});
WithError.args = {
  ...Default.args,
  error: { message: "Field is required." },
};
