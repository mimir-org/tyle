import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AuthContent } from "features/auth/common/auth-content/AuthContent";

export default {
  title: "Features/Auth/Common/AuthContent",
  component: AuthContent,
} as ComponentMeta<typeof AuthContent>;

const Template: ComponentStory<typeof AuthContent> = (args) => <AuthContent {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
