import { faker } from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { NodePreview } from "common/components/node/NodePreview";
import { mockNodeTerminalItem } from "common/utils/mocks";
import { LibraryIcon } from "complib/assets";

export default {
  title: "Common/Node/NodePreview",
  component: NodePreview,
} as ComponentMeta<typeof NodePreview>;

const Template: ComponentStory<typeof NodePreview> = (args) => <NodePreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Node",
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: LibraryIcon,
  terminals: [...Array(7)].map((_) => mockNodeTerminalItem()),
};
