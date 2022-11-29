import { ComponentStory } from "@storybook/react";
import { AccessPlaceholder } from "features/settings/access/placeholder/AccessPlaceholder";

export default {
  title: "Features/Settings/Access/AccessPlaceholder",
  component: AccessPlaceholder,
};

const Template: ComponentStory<typeof AccessPlaceholder> = (args) => <AccessPlaceholder {...args}></AccessPlaceholder>;

export const Default = Template.bind({});
Default.args = {
  text: "No new users have requested access.",
};
