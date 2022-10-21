import { ComponentMeta, ComponentStory } from "@storybook/react";
import { UnauthenticatedContentHeader } from "./UnauthenticatedContentHeader";

export default {
  title: "Content/App/Unauthenticated/UnauthenticatedContentHeader",
  component: UnauthenticatedContentHeader,
} as ComponentMeta<typeof UnauthenticatedContentHeader>;

const Template: ComponentStory<typeof UnauthenticatedContentHeader> = (args) => (
  <UnauthenticatedContentHeader {...args} />
);

export const Default = Template.bind({});
Default.args = {
  title: "Title",
  subtitle: "Subtitle",
};
