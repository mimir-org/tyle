import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AuthContentHeader } from "features/auth/common/auth-content/AuthContentHeader";

export default {
  title: "Features/Auth/Common/AuthContentHeader",
  component: AuthContentHeader,
} as ComponentMeta<typeof AuthContentHeader>;

const Template: ComponentStory<typeof AuthContentHeader> = (args) => <AuthContentHeader {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
