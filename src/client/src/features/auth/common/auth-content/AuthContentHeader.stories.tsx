import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AuthContentHeader } from "./AuthContentHeader";

export default {
  title: "Auth/Common/AuthContentHeader",
  component: AuthContentHeader,
} as ComponentMeta<typeof AuthContentHeader>;

const Template: ComponentStory<typeof AuthContentHeader> = (args) => (
  <AuthContentHeader {...args} />
);

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
