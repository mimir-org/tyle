import { Meta, StoryFn } from "@storybook/react";
import { FormErrorBanner } from "complib/form/FormErrorBanner";

export default {
  title: "Form/FormErrorBanner",
  component: FormErrorBanner,
} as Meta<typeof FormErrorBanner>;

const Template: StoryFn<typeof FormErrorBanner> = (args) => <FormErrorBanner {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "An error occurred when submitting the form.",
};
