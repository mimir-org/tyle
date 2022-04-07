import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FormFieldset } from "./FormFieldset";
import { Default as FormField, WithError as FormFieldWithError } from "./FormField.stories";

export default {
  title: "Form/FormFieldset",
  component: FormFieldset,
} as ComponentMeta<typeof FormFieldset>;

const Template: ComponentStory<typeof FormFieldset> = (args) => <FormFieldset {...args} />;

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
