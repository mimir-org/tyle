import faker from "@faker-js/faker";
import { ComponentMeta, ComponentStory } from "@storybook/react";
import { NodePreview } from "./NodePreview";
import { mockTerminalItem } from "../../../../../../utils/mocks";

export default {
  title: "Content/Home/About/Node/NodePreview",
  component: NodePreview,
  args: {
    color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
    img: "static/media/src/assets/icons/modules/library.svg",
    terminals: [...Array(7)].map((_) => mockTerminalItem()),
  },
} as ComponentMeta<typeof NodePreview>;

const Template: ComponentStory<typeof NodePreview> = (args) => <NodePreview {...args} />;

export const Default = Template.bind({});
