﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtryzeDLL.Plugins
{
    /// <summary>
    /// An interface for a class which handles data read from plugins.
    /// </summary>
    public interface IPluginVisitor
    {
        /// <summary>
        /// Called when a plugin is started to be read.
        /// </summary>
        /// <param name="baseSize">The size of the class's base metadata.</param>
        /// <returns>False if the contents of the plugin should not be read.</returns>
        bool EnterPlugin(int baseSize);
        
        /// <summary>
        /// Called after the contents of a plugin have been read.
        /// </summary>
        void LeavePlugin();

        /// <summary>
        /// Called before a group of revisions begins.
        /// </summary>
        /// <returns>False if the revisions should be skipped.</returns>
        bool EnterRevisions();

        /// <summary>
        /// Called when a revision tag is encountered in the plugin.
        /// </summary>
        /// <param name="revision">Information about the revision.</param>
        void VisitRevision(PluginRevision revision);

        /// <summary>
        /// Called after a group of revisions has been read.
        /// </summary>
        void LeaveRevisions();

        /// <summary>
        /// Called when a comment tag is encountered in the plugin.
        /// </summary>
        /// <param name="title">The comment's title.</param>
        /// <param name="text">The comment's text.</param>
        void VisitComment(string title, string text);

        // These are called whenever basic values are found in the plugin.
        // Parameters should be fairly self-explanatory.
        void VisitUInt8(string name, uint offset, bool visible, uint pluginLine);
        void VisitInt8(string name, uint offset, bool visible, uint pluginLine);
        void VisitUInt16(string name, uint offset, bool visible, uint pluginLine);
        void VisitInt16(string name, uint offset, bool visible, uint pluginLine);
        void VisitUInt32(string name, uint offset, bool visible, uint pluginLine);
        void VisitInt32(string name, uint offset, bool visible, uint pluginLine);
        void VisitFloat32(string name, uint offset, bool visible, uint pluginLine);
        void VisitUndefined(string name, uint offset, bool visible, uint pluginLine);

        void VisitVector3(string name, uint offset, bool visible, uint pluginLine);
        void VisitStringID(string name, uint offset, bool visible, uint pluginLine);
        void VisitTagReference(string name, uint offset, bool visible, bool withClass, bool showJumpTo, uint pluginLine);
        void VisitDataReference(string name, uint offset, bool visible, uint pluginLine);

        /// <summary>
        /// Called when a raw data block is encountered in the plugin.
        /// </summary>
        /// <param name="name">The block's name.</param>
        /// <param name="offset">The block's offset.</param>
        /// <param name="visible">True if the block is visible.</param>
        /// <param name="size">The size of the block.</param>
        void VisitRawData(string name, uint offset, bool visible, int size, uint pluginLine);

        /// <summary>
        /// Called when a ranged value is encountered in the plugin.
        /// </summary>
        /// <param name="name">The value's name.</param>
        /// <param name="offset">The value's offset.</param>
        /// <param name="visible">True if the value is visible.</param>
        /// <param name="type">The base type of the value.</param>
        /// <param name="min">The minimum possible value.</param>
        /// <param name="max">The maximum possible value.</param>
        /// <param name="smallChange">The amount to change the value between small tick marks.</param>
        /// <param name="largeChange">The amount to change the value between large tick marks.</param>
        void VisitRange(string name, uint offset, bool visible, string type, double min, double max,
            double smallChange, double largeChange, uint pluginLine);

        /// <summary>
        /// Called when an ASCII string is encountered in the plugin.
        /// </summary>
        /// <param name="name">The name of the string.</param>
        /// <param name="offset">The offset of the string.</param>
        /// <param name="visible">True if the string is visible.</param>
        /// <param name="length">The length of the string.</param>
        void VisitAscii(string name, uint offset, bool visible, int length, uint pluginLine);

        /// <summary>
        /// Called when a color8, color16, color24, or color32 is encountered in the plugin.
        /// </summary>
        /// <param name="name">The name of the color.</param>
        /// <param name="offset">The offset of the color.</param>
        /// <param name="visible">True if the color entry is visible.</param>
        /// <param name="format">The format of the color, expressed as a string
        /// containing the characters 'r', 'g', 'b', and 'a'. It is guaranteed
        /// to be valid.</param>
        void VisitColorInt(string name, uint offset, bool visible, string format, uint pluginLine);

        /// <summary>
        /// Called when a colorf is encountered in the plugin.
        /// </summary>
        /// <param name="name">The name of the color.</param>
        /// <param name="offset">The offset of the color.</param>
        /// <param name="visible">True if the color entry is visible.</param>
        /// <param name="format">The format of the color, expressed as a string
        /// containing the characters 'r', 'g', 'b', and 'a'. It is guaranteed
        /// to be valid.</param>
        void VisitColorF(string name, uint offset, bool visible, string format, uint pluginLine);

        // These are called whenever a bitfield is found in the plugin.
        // Return false from one of these methods to skip over the
        // bits in the bitfield.
        bool EnterBitfield8(string name, uint offset, bool visible, uint pluginLine);
        bool EnterBitfield16(string name, uint offset, bool visible, uint pluginLine);
        bool EnterBitfield32(string name, uint offset, bool visible, uint pluginLine);

        /// <summary>
        /// Called when a bit definition is encountered inside a bitfield.
        /// </summary>
        /// <param name="name">The bit's name.</param>
        /// <param name="index">The bit's zero-based index (0 = LSB).</param>
        void VisitBit(string name, int index);

        /// <summary>
        /// Called when a bitfield definition is exited.
        /// </summary>
        void LeaveBitfield();

        // These are called whenever an enum is found in the plugin.
        // Return false from one of these methods to skip over the
        // options in the enum.
        bool EnterEnum8(string name, uint offset, bool visible, uint pluginLine);
        bool EnterEnum16(string name, uint offset, bool visible, uint pluginLine);
        bool EnterEnum32(string name, uint offset, bool visible, uint pluginLine);

        /// <summary>
        /// Called when an enum option definition is encountered inside an enum.
        /// </summary>
        /// <param name="name">The option's name.</param>
        /// <param name="value">The option's value.</param>
        void VisitOption(string name, int value);

        /// <summary>
        /// Called when an enum definition is exited.
        /// </summary>
        void LeaveEnum();

        /// <summary>
        /// Called when a reflexive definition is entered.
        /// </summary>
        /// <param name="name">The reflexive's name.</param>
        /// <param name="offset">The offset of the reflexive's size and pointer.</param>
        /// <param name="visible">True if the reflexive is visible.</param>
        /// <param name="entrySize">The size of each entry in the reflexive.</param>
        /// <returns>False if the entries in the reflexive should be skipped over.</returns>
        bool EnterReflexive(string name, uint offset, bool visible, uint entrySize, uint pluginLine);

        /// <summary>
        /// Called when a reflexive definition is exited.
        /// </summary>
        void LeaveReflexive();
    }
}