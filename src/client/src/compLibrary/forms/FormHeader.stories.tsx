import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FormHeader } from "./FormHeader";

export default {
  title: "Forms/FormHeader",
  component: FormHeader,
} as ComponentMeta<typeof FormHeader>;

const Template: ComponentStory<typeof FormHeader> = (args) => <FormHeader {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "This is the title",
};

export const WithSubtitle = Template.bind({});
WithSubtitle.args = {
  ...Default.args,
  subtitle: "This is the subtitle",
};
