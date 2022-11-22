import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Form } from "complib/form/Form";
import { Default as FormErrorBanner } from "complib/form/FormErrorBanner.stories";
import { Default as FormFieldset, WithError as FormFieldsetWithError } from "complib/form/FormFieldset.stories";
import { WithSubtitle as FormHeader } from "complib/form/FormHeader.stories";

export default {
  title: "Form/Form",
  component: Form,
} as ComponentMeta<typeof Form>;

const Template: ComponentStory<typeof Form> = (args) => <Form {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: (
    <>
      <FormHeader {...FormHeader.args} />
      <FormFieldset {...FormFieldset.args} />
    </>
  ),
};

export const WithError = Template.bind({});
WithError.args = {
  children: (
    <>
      <FormHeader {...FormHeader.args} />
      <FormErrorBanner {...FormErrorBanner.args} />
      <FormFieldsetWithError {...FormFieldsetWithError.args} />
    </>
  ),
};
