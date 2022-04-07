import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FormErrorBanner } from "./FormErrorBanner";

export default {
  title: "Form/FormErrorBanner",
  component: FormErrorBanner,
} as ComponentMeta<typeof FormErrorBanner>;

const Template: ComponentStory<typeof FormErrorBanner> = (args) => <FormErrorBanner {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "An error occurred when submitting the form.",
};
