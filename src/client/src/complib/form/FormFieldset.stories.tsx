import { Meta, StoryFn } from "@storybook/react";
import { Default as FormField, WithError as FormFieldWithError } from "complib/form/FormField.stories";
import { FormFieldset } from "complib/form/FormFieldset";

export default {
  title: "Form/FormFieldset",
  component: FormFieldset,
} as Meta<typeof FormFieldset>;

const Template: StoryFn<typeof FormFieldset> = (args) => <FormFieldset {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: (
    <>
      <FormField {...FormField.args} />
      <FormField {...FormField.args} />
      <FormField {...FormField.args} />
    </>
  ),
};

export const WithError = Template.bind({});
WithError.args = {
  children: (
    <>
      <FormField {...FormField.args} />
      <FormFieldWithError {...FormFieldWithError.args} />
      <FormField {...FormField.args} />
      <FormFieldWithError {...FormFieldWithError.args} />
    </>
  ),
};
