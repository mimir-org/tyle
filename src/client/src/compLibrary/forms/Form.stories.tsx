import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Form } from "./Form";
import { WithSubtitle as FormHeader } from "./FormHeader.stories";
import { Default as FormFieldset, WithError as FormFieldsetWithError } from "./FormFieldset.stories";
import { Default as FormErrorBanner } from "./FormErrorBanner.stories";

export default {
  title: "Forms/Form",
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
