import { ComponentStory } from "@storybook/react";
import { AccessCardDetails } from "features/settings/access/card/details/AccessCardDetails";

export default {
  title: "Settings/Access/AccessCardDetails",
  component: AccessCardDetails,
};

const Template: ComponentStory<typeof AccessCardDetails> = (args) => <AccessCardDetails {...args}></AccessCardDetails>;

export const Default = Template.bind({});
Default.args = {
  descriptors: {
    "E-mail": "jane.smith@organization.com",
    Organization: "Mimirorg Company",
    Purpose: "I want to create entities on behalf of my employer",
  },
};
