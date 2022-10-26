import { ComponentMeta, ComponentStory } from "@storybook/react";
import { AuthContent } from "./AuthContent";

export default {
  title: "Auth/Common/AuthContent",
  component: AuthContent,
} as ComponentMeta<typeof AuthContent>;

const Template: ComponentStory<typeof AuthContent> = (args) => <AuthContent {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
