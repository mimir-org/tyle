import { ComponentStory } from "@storybook/react";
import { AccessCardHeader } from "features/settings/access/card/header/AccessCardHeader";

export default {
  title: "Settings/Access/AccessCardHeader",
  component: AccessCardHeader,
};

const Template: ComponentStory<typeof AccessCardHeader> = (args) => <AccessCardHeader {...args}></AccessCardHeader>;

export const Default = Template.bind({});
Default.args = {
  children: "Jane Smith",
};
