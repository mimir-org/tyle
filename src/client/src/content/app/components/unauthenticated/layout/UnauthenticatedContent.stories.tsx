import { ComponentMeta, ComponentStory } from "@storybook/react";
import { UnauthenticatedContent } from "./UnauthenticatedContent";

export default {
  title: "Content/App/Unauthenticated/UnauthenticatedContent",
  component: UnauthenticatedContent,
} as ComponentMeta<typeof UnauthenticatedContent>;

const Template: ComponentStory<typeof UnauthenticatedContent> = (args) => <UnauthenticatedContent {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
