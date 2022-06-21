import faker from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockTerminalItem } from "../../../../../../utils/mocks";
import { NodePreview } from "./NodePreview";

export default {
  title: "Content/Home/About/Node/NodePreview",
  component: NodePreview,
} as ComponentMeta<typeof NodePreview>;

const Template: ComponentStory<typeof NodePreview> = (args) => <NodePreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Node",
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: "static/media/src/assets/icons/modules/library.svg",
  terminals: [...Array(7)].map((_) => mockTerminalItem()),
};
