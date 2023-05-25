import { Meta, StoryFn } from "@storybook/react";
import { FormHeader } from "complib/form/FormHeader";

export default {
  title: "Form/FormHeader",
  component: FormHeader,
} as Meta<typeof FormHeader>;

const Template: StoryFn<typeof FormHeader> = (args) => <FormHeader {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "This is the title",
};

export const WithSubtitle = Template.bind({});
WithSubtitle.args = {
  ...Default.args,
  subtitle: "This is the subtitle",
};
