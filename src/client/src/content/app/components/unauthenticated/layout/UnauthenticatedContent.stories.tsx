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
  infoTitle: "Info title",
  infoText: "Some descriptive text about the extra info can med placed here",
};

export const WithAction = Template.bind({});
WithAction.args = {
  ...Default.args,
  actionable: true,
  actionText: "Action",
  onAction: () => alert("[STORYBOOK] UnauthenticatedContent.onAction"),
};
