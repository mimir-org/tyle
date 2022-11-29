import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { InterfacePreview } from "features/common/interface/InterfacePreview";

export default {
  title: "Features/Common/Interface/InterfacePreview",
  component: InterfacePreview,
} as ComponentMeta<typeof InterfacePreview>;

const Template: ComponentStory<typeof InterfacePreview> = (args) => <InterfacePreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Interface",
  aspectColor: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  interfaceColor: faker.color.rgb(),
};
